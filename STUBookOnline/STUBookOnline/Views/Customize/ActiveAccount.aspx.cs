using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace STUBookOnline.Views.Customize
{
    public partial class ActiveAccount : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string idAccount = Request.QueryString["id"]; // note truoc: id và key này đưa xuống config

                string key1 = Request.QueryString["AccesssKey"];


                bool b = String.IsNullOrEmpty(idAccount) || String.IsNullOrEmpty(key1);
                if (b)
                {
                    Response.Redirect(""); // go to home page //
                    return;
                }
                else
                {
                    //check db = linq 
                    // tao lop khac de check vs update nha
                    //update db = linq
                }
            }
        }

        private void ShowConfig()
        {
            List<String> lst = new List<string>();
            // For read access you do not need to call the OpenExeConfiguraton
            foreach (string key in ConfigurationManager.AppSettings)
            {
                string value = ConfigurationManager.AppSettings[key];
                lst.Add(String.Format("Key: {0}, Value: {1}", key, value));

            }
        }

        private void UpdateConfig(string usID, string key)
        {
            // Open App.Config of executable
            System.Configuration.Configuration config =
              ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            // Add an Application Setting.
            config.AppSettings.Settings["id"].Value = usID;
            config.AppSettings.Settings["AccessKey"].Value = key;
            // Save the configuration file.
            config.Save(ConfigurationSaveMode.Modified, true);
            // Force a reload of a changed section.
            ConfigurationManager.RefreshSection("appSettings");
        }
    }
}