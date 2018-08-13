using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Form_Builder_App.Models
{
    public class formDetails
    {

        public formDetails(int _formId, string _formName, int _numOfSubmissions)
        {
            this.formID = _formId;
            this.formName = _formName;
            this.numOfSubmissions = _numOfSubmissions;
        }
        [Display(Name = "Form ID")]
        public int formID { get; set; }
        [Display(Name = "Form Name")]
        public string formName { get; set; }
        [Display(Name = "#Submissions")]

        public int numOfSubmissions { get; set; }
    }
    public class formCreateModel
    {
        [Display(Name = "Form Name")]
        public string formName { get; set; }
        public formItemModel[] formItems { get; set; }
        public formCreateModel()
        {

        }
        public formCreateModel(string _formName, formItemModel[] _formItems)
        {
            this.formName = _formName;
            this.formItems = _formItems;
        }

        public bool HasDuplicateItems()
        {
            var duplicates = formItems
             .GroupBy(p =>  new { p.inputName })
             .Where(g => g.Count() > 1)
             .Select(g => g.Key);


            return (duplicates.Count() > 0);
        }
    }
    public class formItemModel
    {
        [Display(Name = "Label")]

        public string fieldLabel { get; set; }
        [Display(Name = "Input Name")]

        public string inputName { get; set; }
        [Display(Name = "Type")]

        public string inputTypeName { get; set; }
        [Display(Name = "Required")]

        public bool required { get; set; }

        public formItemModel()
        {

        }
        public formItemModel(string _fieldLabel, string _inputName, string _inputTypeName,bool _required)
        {
            this.fieldLabel = _fieldLabel;
            this.inputName = _inputName;
            this.inputTypeName = _inputTypeName;
            this.required = _required;
        }
        public bool isValidItem()
        {
            if (this.fieldLabel == null || this.fieldLabel == "")
                return false;
            if (this.inputTypeName == null || this.inputTypeName == "")
                return false;
            if (this.inputName == null || this.inputName == "")
                return false;
            return true;
        }
    }
    public class userSubmissionKeyModel
    {
        public int userID { get; set; }
        public DateTime submissionDate { get; set; }
        public userSubmissionKeyModel()
        {

        }
        public userSubmissionKeyModel(int _userID, DateTime _submissionDate)
        {
            this.userID = _userID;
            this.submissionDate = _submissionDate;
        }
    }
}