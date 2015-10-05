<%@ WebService Language="C#" Class="Quartz.Services.Learn" %>

using System.Collections;
using System;
using System.Web;
using System.Web.Services;
using System.Xml;
using System.Web.Services.Protocols;
using System.Web.Script.Services;

using Quartz.Portal;
using Quartz.Social;
using Quartz.Learning;
using Quartz.Communication;
using Quartz.Organization;

namespace Quartz.Services
{
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ScriptService]
    public class Learn : System.Web.Services.WebService
    {
        [WebMethod]
        public string[] GetUserTrainingSlide(int user_id, int slide_id, int training_id, int feed_id, bool alt_mode)
        {
            bool prior_exists = false;
            bool next_exists = false;
            int total_slides = 0;
            string slide_type = string.Empty;
            string slide_content = string.Empty;
            string final_slide_content = string.Empty;
            string slide_title = string.Empty;
            string final_slide_title = string.Empty;
            int prior_slide_order = 0;
            int next_slide_order = 0;
            
            qLrn_UserTraining_View training = new qLrn_UserTraining_View(user_id, training_id);

            var slide = new qLrn_TrainingSlide(); 
            var prior_slide = new qLrn_TrainingSlide();
            var next_slide = new qLrn_TrainingSlide();

            if (slide_id == 0)
                slide = qLrn_TrainingSlide.GetFirstSlideInTraining(training_id);
            else
                slide = qLrn_TrainingSlide.GetTrainingSlideBySlideID(slide_id);
            
            var all_slides = qLrn_TrainingSlide_View.GetSlides(training_id);

            total_slides = all_slides.Count;
            if (total_slides > 1 && slide.SlideOrder < total_slides)
            {
                next_exists = true;
                next_slide_order = Convert.ToInt32(slide.SlideOrder) + 1;
                next_slide = qLrn_TrainingSlide.GetTrainingSlideBySlideOrder(training_id, next_slide_order);
            }
            if (total_slides > 1)
            {
                prior_exists = true;
                prior_slide_order = Convert.ToInt32(slide.SlideOrder) - 1;
                prior_slide = qLrn_TrainingSlide.GetTrainingSlideBySlideOrder(training_id, prior_slide_order);
            }

            if (alt_mode == true)
            {
                if (!String.IsNullOrEmpty(slide.SimpleViewType))
                slide_type = slide.SimpleViewType;
                slide_content = slide.SimpleViewContent;
                slide_title = slide.Title;
            }

            if (!String.IsNullOrEmpty(slide_content))
                final_slide_content = HttpUtility.HtmlEncode(slide_content);
            if (!String.IsNullOrEmpty(slide_title))
                final_slide_title = HttpUtility.HtmlEncode(slide_title);

            string[] slide_info = new string[13];

            slide_info[0] = Convert.ToString(slide_id);
            slide_info[1] = Convert.ToString(training_id);
            slide_info[2] = Convert.ToString(prior_exists);
            slide_info[3] = Convert.ToString(next_exists);
            slide_info[4] = Convert.ToString(slide_type);
            slide_info[5] = Convert.ToString(final_slide_content);
            slide_info[6] = Convert.ToString(final_slide_title);
            if (prior_slide == null)
                slide_info[7] = "0";
            else
                slide_info[7] = Convert.ToString(prior_slide.SlideID);
            if (next_slide == null)
                slide_info[8] = "0";
            else
                slide_info[8] = Convert.ToString(next_slide.SlideID);
            slide_info[9] = Convert.ToString(user_id);
            slide_info[10] = Convert.ToString(feed_id);
            slide_info[11] = Convert.ToString(alt_mode);
            slide_info[12] = Convert.ToString(slide.SlideOrder);
            
            // update training status
            var u_training = qLrn_UserTraining.GetUserTraining(user_id, training_id);
            u_training.MostRecentSlide = slide_id;
            u_training.MostRecentSlideTime = DateTime.Now;
            if (u_training.FurthestSlide > 0)
            {
                var furthest_slide = qLrn_TrainingSlide.GetTrainingSlideBySlideID(u_training.FurthestSlide);
                if (furthest_slide != null)
                {
                    if (furthest_slide.SlideID > 0)
                    {
                        int furthest_order_order = furthest_slide.SlideOrder;
                        if (furthest_order_order < slide.SlideOrder)
                        {
                            u_training.FurthestSlide = slide.SlideID;
                            u_training.FurthestSlideTime = DateTime.Now;
                        }  
                    }
                }
            }
            
            // check to see if last slide in training, if so mark training as complete and add log
            if (next_slide == null)
            {
                if (u_training.Status != "Completed")
                {
                    u_training.EndTime = DateTime.Now;
                    u_training.Status = "Completed";
                    u_training.Completed = DateTime.Now;

                    // create training finished log
                    qPtl_Log log = new qPtl_Log();
                    qPtl_LogAction logAction = new qPtl_LogAction("Complete Internal Training");
                    log.Insert(user_id, 1, "Yes", DateTime.Now, user_id, null, -1, 0, logAction.LogActionID, (int)qSoc_ContentType.Types.Training, training_id);

                    // run training workflows
                    var workflows = qPtl_Workflow.GetWorkflows((int)qSoc_ContentType.Types.Training, logAction.LogActionID, training_id);
                    foreach (var i in workflows)
                    {
                        string returnMessage = qPtl_Workflow.ProcessWorkflow(i.WorkflowID, user_id);
                    }
                }
            }

            u_training.LastModified = DateTime.Now;
            u_training.LastModifiedBy = user_id;
            u_training.Update();
                
            return slide_info;
        }
    }
}