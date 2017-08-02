using System.Collections.Generic;

namespace NH_UI.Factory
{
    public class ButtonCategory
    {
        public List<SubCategory> SubCategories = new List<SubCategory>();
        public string CategoryName;
        public ButtonCategory(string name)
        {
            CategoryName = name;
        }
    }
}
