using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Form_Builder_App.Models;
using System.Web.Helpers;

namespace Form_Builder_App.Controllers
{
    public class formsController : Controller
    {
        public formBuilderDBEntities1 db = new formBuilderDBEntities1();
        // GET: tblForms
        public ActionResult Index()
        {
            if (Session["logged_in_user"] == null)
            {
                ViewBag.Error = "";
                return RedirectToAction("Index", "Home", new { errorMsg = "Access Denied, need to login" });
            }
            List<tblForm> listForms = db.tblForms.Select(m => m).ToList();
            List<formDetails> listFormDetails = new List<formDetails>();
            foreach (var item in listForms)
            {
                int numOfSubmissions = db.tblUserSubmissions.Where(m => m.formId == item.formId).Select(m => m.submissionDate).Distinct().ToList().Count();
                listFormDetails.Add(new formDetails(item.formId, item.formName, numOfSubmissions));
            }
            return View(listFormDetails);
        }


        // GET: tblForms/Create
        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.listInputTypes = new List<string>(new string[] { "text", "color", "date", "email", "tel", "number" });
            return View();
        }

        // POST: tblForms/Create
        [HttpPost]
        public ActionResult Create(formCreateModel formCreate)
        {
            if (!isValidInput(formCreate))
            {
                return Json(false);
            }
            string formName = formCreate.formName;
            formItemModel[] arrFormItems = formCreate.formItems;

            int formID = addFormNameToDB(formName);
            for (int pos = 0; pos < arrFormItems.Length; pos++)
            {
                tblInputInForm tblInputInForm = new tblInputInForm(formID, arrFormItems[pos].inputTypeName, pos, arrFormItems[pos].fieldLabel, arrFormItems[pos].inputName, arrFormItems[pos].required);
                db.tblInputInForms.Add(tblInputInForm);
            }
            db.SaveChanges();
            return Json(true);
        }


        private bool isValidInput(formCreateModel formCreate)
        {
            if (!isFormNameValid(formCreate.formName))
                return false;
            formItemModel[] formItems = formCreate.formItems;
            if (formCreate.HasDuplicateItems())
            {
                addError("There are duplicate items in form.");
                return false;
            }
            foreach (formItemModel item in formItems)
            {
                if (!item.isValidItem())
                {
                    addError("Field Label  '" + item.fieldLabel + "', Input Name : '" + item.inputName + "', Input Type : '" + item.inputTypeName + "' has invalid inputs.");
                    return false;
                }

            }
            return true;

        }

        private bool isFormNameValid(string formName)
        {
            if (formName == null || formName == "")
            {
                addError("Must enter form name.");
                return false;
            }
            var listForms = db.tblForms.Where(m => m.formName == formName).Select(m => m).ToList();
            if (listForms.Count > 0)
            {
                addError("Form name " + formName + " already exists.");
                return false;
            }
            return true;
        }

        private int addFormNameToDB(string formName)
        {
            try
            {
                db.tblForms.Add(new tblForm(formName));
                db.SaveChanges();
                int formID = db.tblForms.Where(m => m.formName == formName).Select(m => m.formId).Single();
                return formID;
            }
            catch (Exception e)
            {
                addError(e.Message);
                return -1;
            }
        }






        // GET: tblForms/Create
        [HttpPost]
        public ActionResult GetInputsInForm(int formID)
        {
            return Json(db.tblInputInForms.Where(m => m.formId == formID).Select(m => new { m.fieldLabel, m.inputName, m.inputTypeName, m.positionInForm, m.required }).OrderBy(m => m.positionInForm).ToArray());
        }







        // GET: tblForms/Create
        [HttpGet]
        public ActionResult SubmitFormPage(int formID)
        {
            ViewBag.formName = db.tblForms.Where(m => m.formId == formID).Select(m => m.formName).Single();
            ViewBag.formID =formID;
            return View();
        }

        // POST: tblForms/Create
        [HttpPost]
        public ActionResult SubmitFormPage(int formID, string[] arrUserInputs)
        {
            try
            {
                int userID = ((tblUser)Session["logged_in_user"]).userID;
                DateTime submissionDateTime = DateTime.Now;
                for (int i = 0; i < arrUserInputs.Length; i++)
                {
                    tblUserSubmission tblUserSubmission = new tblUserSubmission(userID, formID, i, submissionDateTime, arrUserInputs[i]);
                    db.tblUserSubmissions.Add(tblUserSubmission);
                }
                db.SaveChanges();
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                return Json(false);

            }
            return Json(true);


        }

        public ActionResult UserSubmissionsPage(int formID)
        {
            List<tblUserSubmission> listUserInputSubmissions = db.tblUserSubmissions.Where(m => m.formId == formID).Select(m => m).ToList();
            string[] listFieldNames = db.tblInputInForms.Where(m => m.formId == formID).Select(m => m.inputName).ToArray();
            userSubmissionKeyModel[] listSubmissionKeys = db.tblUserSubmissions.Where(m => m.formId == formID).Select(m => new userSubmissionKeyModel {userID =  m.userId , submissionDate = m.submissionDate}).Distinct().ToArray();
            Dictionary<userSubmissionKeyModel, string[]> dicUserInputs = new Dictionary<userSubmissionKeyModel, string[]>();
            Dictionary<int, string> dicUserNames = new Dictionary<int, string>();
            foreach (userSubmissionKeyModel key in listSubmissionKeys)
            {
                string[] userInputs = listUserInputSubmissions.Where(m => m.submissionDate == key.submissionDate && m.userId == key.userID).Select(m => m.userInput).ToArray();
                dicUserInputs[key] = userInputs;
                dicUserNames[key.userID] = db.tblUsers.Where(m => m.userID == key.userID).Select(m => m.userName).Single() ;
            }
            ViewBag.dicUserNames = dicUserNames;
            ViewBag.dicUserInputs = dicUserInputs;
            ViewBag.listFieldNames = listFieldNames;
            return View();
        }


        public ActionResult TemplateView()
        {
            return View();
        }





























        public void addError(string errorMsg)
        {
            ViewBag.Error = true;
            ViewBag.ErrorDetails += errorMsg;
        }
        private void addInfo(string info)
        {
            ViewBag.Info = true;
            ViewBag.InfoDetails += info;
        }







    }
}
