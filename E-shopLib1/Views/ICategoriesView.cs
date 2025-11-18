using System;
using System.Collections.Generic;

namespace E_shopLib
{
    public interface ICategoriesView
    {
        void ShowCategories(List<string> categories);
        event Action<string> CategorySelected;
    }
}