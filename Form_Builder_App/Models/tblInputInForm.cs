//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Form_Builder_App.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class tblInputInForm
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tblInputInForm()
        {
            this.tblUserSubmissions = new HashSet<tblUserSubmission>();
        }
        public tblInputInForm(int _formId, string _inputTypeName, int _positionInForm, string _fieldLabel, string _inputName, bool _required)
        {
            this.formId = _formId;
            this.inputTypeName = _inputTypeName;
            this.positionInForm = _positionInForm;
            this.fieldLabel = _fieldLabel;
            this.inputName = _inputName;
            this.required = _required;
        }
        public int formId { get; set; }
        public string inputTypeName { get; set; }
        public int positionInForm { get; set; }
        public string fieldLabel { get; set; }
        public string inputName { get; set; }
        public bool required { get; set; }
    
        public virtual tblForm tblForm { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblUserSubmission> tblUserSubmissions { get; set; }
    }
}
