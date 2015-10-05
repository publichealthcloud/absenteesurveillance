using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public interface IQuestionElementEditControl
{
    string GetDetails();
    bool IsCorrect();
    int GetQuestionElementId();
    void SetDetails(string details);
    void SetCorrect(bool is_correct);
    void SetQuestionElementId(int question_element_id);
    int GetOrderNumber();
}

public partial class slide_modules_question_element_edit : System.Web.UI.UserControl, IQuestionElementEditControl
{
    protected int order_number;

    public int OrderNumber
    {
        get { return order_number; }
        set { order_number = value; }
    }
    
    protected int QuestionElementID
    {
        get { return Convert.ToInt32(ViewState["question_element_id"]); }
        set { ViewState ["question_element_id"] = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (order_number == 1)
        {
            litChoiceLetter.Text = "A)";
            lblCorrectText.Text = "Correct Choice is A";
        }
        else if (order_number == 2)
        {
            litChoiceLetter.Text = "B)";
            lblCorrectText.Text = "Correct Choice is B";
        }
        else if (order_number == 3)
        {
            litChoiceLetter.Text = "C)";
            lblCorrectText.Text = "Correct Choice is C";
        }
        else if (order_number == 4)
        {
            litChoiceLetter.Text = "D)";
            lblCorrectText.Text = "Correct Choice is D";
        }
        else if (order_number == 5)
        {
            litChoiceLetter.Text = "E)";
            lblCorrectText.Text = "Correct Choice is E";
        }
        else if (order_number == 6)
        {
            litChoiceLetter.Text = "F)";
            lblCorrectText.Text = "Correct Choice is F";
        }
    }

    public string GetDetails()
    {
        return txt_details.Text;
    }

    public bool IsCorrect()
    {
        return cb_is_correct.Checked;
    }

    public int GetQuestionElementId()
    {
        return QuestionElementID;
    }

    public int GetOrderNumber()
    {
        return OrderNumber;
    }

    public void SetDetails(string details)
    {
        txt_details.Text = details;
    }

    public void SetCorrect(bool is_correct)
    {
        cb_is_correct.Checked = is_correct;
    }

    public void SetQuestionElementId(int question_element_id)
    {
        QuestionElementID = question_element_id;
    }
}