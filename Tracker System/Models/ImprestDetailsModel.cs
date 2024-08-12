using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tracker_System.Models
{
    public class ImprestDetailsModel
    {
        public string DivisionID { get; set; }
        public string DivisionName { get; set; }
        public string CompanyID { get; set; }
        public string CompanyName { get; set; }
        public string No { get; set; }
        public string EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public string DesignationId { get; set; }
        public string DesignationName { get; set; }
        public string ZoneId { get; set; }
        public string ZoneName { get; set; }
        public string RegionId { get; set; }
        public string RegionName { get; set; }
        public List<DivisionModel> lstDivision { get; set; }
        public List<CompanyModel> lstCompany { get; set; }
        public List<ImprestDetailsModel> ImprestDetailsList { get; set; }
        public List<EmployeeModel> EmployeeList { get; set; }


    }
    public class DivisionModel
    {
        public string ID { get; set; }
        public string Name { get; set; }
    }
    public class CompanyModel
    {
        public string ID { get; set; }
        public string Name { get; set; }
    }
    public class EmployeeModel
    {
        public string ID { get; set; }
        public string Name { get; set; }
    }
}