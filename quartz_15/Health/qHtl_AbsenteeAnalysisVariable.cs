using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Quartz.Health
{
    public class qHtl_AbsenteeAnalysisVariable
    {
        protected static qHtl_AbsenteeAnalysisVariable schema = new qHtl_AbsenteeAnalysisVariable();

        protected DbRow container;
        protected readonly DbColumn<Int32> absentee_analysis_variable_id;
        protected readonly DbColumn<Int32> school_district_id;
        protected readonly DbColumn<DateTime?> solution_start_date;
        protected readonly DbColumn<Decimal> green_rate_std_multiplier;
        protected readonly DbColumn<Decimal> yellow_rate_std_multiplier;
        protected readonly DbColumn<Decimal> red_rate_std_multiplier;
        protected readonly DbColumn<Decimal> green_illness_boundary;
        protected readonly DbColumn<Decimal> red_illness_boundary;
        protected readonly DbColumn<Decimal> yellow_illness_boundary;
        protected readonly DbColumn<Decimal> green_gastrointestinal_boundary;
        protected readonly DbColumn<Decimal> red_gastrointestinal_boundary;
        protected readonly DbColumn<Decimal> yellow_gastrointestinal_boundary;
        protected readonly DbColumn<Decimal> green_respiratory_boundary;
        protected readonly DbColumn<Decimal> red_respiratory_boundary;
        protected readonly DbColumn<Decimal> yellow_respiratory_boundary;
        protected readonly DbColumn<Decimal> green_rash_boundary;
        protected readonly DbColumn<Decimal> yellow_rash_boundary;
        protected readonly DbColumn<Decimal> red_rash_boundary;
        protected readonly DbColumn<Decimal> green_other_illness_boundary;
        protected readonly DbColumn<Decimal> yellow_other_illness_boundary;
        protected readonly DbColumn<Decimal> red_other_illness_boundary;
        protected readonly DbColumn<Decimal> green_unknown_illness_boundary;
        protected readonly DbColumn<Decimal> yellow_unknown_illness_boundary;
        protected readonly DbColumn<Decimal> red_unknown_illness_boundary;
        protected readonly DbColumn<String> sd_formula_type;

        public Int32 AbsenteeAnalysisVariableID { get { return absentee_analysis_variable_id.Value; } set { absentee_analysis_variable_id.Value = value; } }
        public Int32 SchoolDistrictID { get { return school_district_id.Value; } set { school_district_id.Value = value; } }
        public DateTime? SolutionStartDate { get { return solution_start_date.Value; } set { solution_start_date.Value = value; } }
        public Decimal GreenRateSTDMultiplier { get { return green_rate_std_multiplier.Value; } set { green_rate_std_multiplier.Value = value; } }
        public Decimal YellowRateSTDMultiplier { get { return yellow_rate_std_multiplier.Value; } set { yellow_rate_std_multiplier.Value = value; } }
        public Decimal RedRateSTDMultiplier { get { return red_rate_std_multiplier.Value; } set { red_rate_std_multiplier.Value = value; } }
        public Decimal GreenIllnessBoundary { get { return green_illness_boundary.Value; } set { green_illness_boundary.Value = value; } }
        public Decimal YellowIllnessBoundary { get { return yellow_illness_boundary.Value; } set { yellow_illness_boundary.Value = value; } }
        public Decimal RedIllnessBoundary { get { return red_illness_boundary.Value; } set { red_illness_boundary.Value = value; } }
        public Decimal GreenGastrointestinalBoundary { get { return green_gastrointestinal_boundary.Value; } set { green_gastrointestinal_boundary.Value = value; } }
        public Decimal YellowGastrointestinalBoundary { get { return yellow_gastrointestinal_boundary.Value; } set { yellow_gastrointestinal_boundary.Value = value; } }
        public Decimal RedGastrointestinalBoundary { get { return red_gastrointestinal_boundary.Value; } set { red_gastrointestinal_boundary.Value = value; } }
        public Decimal GreenRespiratoryBoundary { get { return green_respiratory_boundary.Value; } set { green_respiratory_boundary.Value = value; } }
        public Decimal YellowRespiratoryBoundary { get { return yellow_respiratory_boundary.Value; } set { yellow_respiratory_boundary.Value = value; } }
        public Decimal RedRespiratoryBoundary { get { return red_respiratory_boundary.Value; } set { red_respiratory_boundary.Value = value; } }
        public Decimal GreenRashBoundary { get { return green_rash_boundary.Value; } set { green_rash_boundary.Value = value; } }
        public Decimal YellowRashBoundary { get { return yellow_rash_boundary.Value; } set { yellow_rash_boundary.Value = value; } }
        public Decimal RedRashBoundary { get { return red_rash_boundary.Value; } set { red_rash_boundary.Value = value; } }
        public Decimal GreenOtherIllnessBoundary { get { return green_other_illness_boundary.Value; } set { green_other_illness_boundary.Value = value; } }
        public Decimal YellowOtherIllnessBoundary { get { return yellow_other_illness_boundary.Value; } set { yellow_other_illness_boundary.Value = value; } }
        public Decimal RedOtherIllnessBoundary { get { return red_other_illness_boundary.Value; } set { red_other_illness_boundary.Value = value; } }
        public Decimal GreenUnknownIllnessBoundary { get { return green_unknown_illness_boundary.Value; } set { green_unknown_illness_boundary.Value = value; } }
        public Decimal YellowUnknownIllnessBoundary { get { return yellow_unknown_illness_boundary.Value; } set { yellow_unknown_illness_boundary.Value = value; } }
        public Decimal RedUnknownIllnessBoundary { get { return red_unknown_illness_boundary.Value; } set { red_unknown_illness_boundary.Value = value; } }
        public String SDFormulaType { get { return sd_formula_type.Value; } set { sd_formula_type.Value = value; } }

        public qHtl_AbsenteeAnalysisVariable()
            : this(new DbRow())
        {
        }

        protected qHtl_AbsenteeAnalysisVariable(DbRow c)
        {
            container = c;
            container.SetContainerName("qHtl_AbsenteeAnalysisVariables");
            absentee_analysis_variable_id = container.NewColumn<Int32>("AbsenteeAnalysisVariableID", true);
            school_district_id = container.NewColumn<Int32>("SchoolDistrictID");
            solution_start_date = container.NewColumn<DateTime?>("SolutionStartDate");
            green_rate_std_multiplier = container.NewColumn<Decimal>("GreenRateSTDMultiplier");
            yellow_rate_std_multiplier = container.NewColumn<Decimal>("YellowRateSTDMultiplier");
            red_rate_std_multiplier = container.NewColumn<Decimal>("RedRateSTDMultiplier");
            green_illness_boundary = container.NewColumn<Decimal>("GreenIllnessBoundary");
            yellow_illness_boundary = container.NewColumn<Decimal>("YellowIllnessBoundary");
            red_illness_boundary = container.NewColumn<Decimal>("RedIllnessBoundary");
            green_gastrointestinal_boundary = container.NewColumn<Decimal>("GreenGastrointestinalBoundary");
            yellow_gastrointestinal_boundary = container.NewColumn<Decimal>("YellowGastrointestinalBoundary");
            red_gastrointestinal_boundary = container.NewColumn<Decimal>("RedGastrointestinalBoundary");
            green_respiratory_boundary = container.NewColumn<Decimal>("GreenRespiratoryBoundary");
            yellow_respiratory_boundary = container.NewColumn<Decimal>("YellowRespiratoryBoundary");
            red_respiratory_boundary = container.NewColumn<Decimal>("RedRespiratoryBoundary");
            green_rash_boundary = container.NewColumn<Decimal>("GreenRashBoundary");
            yellow_rash_boundary = container.NewColumn<Decimal>("YellowRashBoundary");
            red_rash_boundary = container.NewColumn<Decimal>("RedRashBoundary");
            green_other_illness_boundary = container.NewColumn<Decimal>("GreenOtherIllnessBoundary");
            yellow_other_illness_boundary = container.NewColumn<Decimal>("YellowOtherIllnessBoundary");
            red_other_illness_boundary = container.NewColumn<Decimal>("RedOtherIllnessBoundary");
            green_unknown_illness_boundary = container.NewColumn<Decimal>("GreenUnknownIllnessBoundary");
            yellow_unknown_illness_boundary = container.NewColumn<Decimal>("YellowUnknownIllnessBoundary");
            red_unknown_illness_boundary = container.NewColumn<Decimal>("RedUnknownIllnessBoundary");
            sd_formula_type = container.NewColumn<String>("SDFormulaType");
        }

        public qHtl_AbsenteeAnalysisVariable(Int32 absentee_analysis_variable_id)
            : this()
        {
            container.Select("AbsenteeAnalysisVariableID = @AbsenteeAnalysisVariableID", new SqlQueryParameter("@AbsenteeAnalysisVariableID", absentee_analysis_variable_id));
        }

        public void Update()
        {
            container.Update("AbsenteeAnalysisVariableID = @AbsenteeAnalysisVariableID");
        }

        public void Insert()
        {
            AbsenteeAnalysisVariableID = Convert.ToInt32(container.Insert());
        }

        public static qHtl_AbsenteeAnalysisVariable GetAbsenteeAnalysisVariablesBySchoolDistrict(int school_district_id)
        {
            qHtl_AbsenteeAnalysisVariable variables = new qHtl_AbsenteeAnalysisVariable();

            variables.container.Select(new DbQuery
            {
                Top = "Top(1)",
                Where = string.Format("SchoolDistrictID = " + school_district_id),
                OrderBy = "AbsenteeAnalysisVariableID DESC"
            });

            if (variables.AbsenteeAnalysisVariableID > 0) return variables;
            else return null;
        }
    }
}
