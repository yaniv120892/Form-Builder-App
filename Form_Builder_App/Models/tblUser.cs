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

    public partial class tblUser
    {

        public tblUser()
        {
        }
        public tblUser(int _userID, string _username, string _password)
        {
            this.userID = _userID;
            this.userName = _username;
            this.password = _password;
        }

        public int userID { get; set; }
        public string userName { get; set; }
        public string password { get; set; }

    }

}
