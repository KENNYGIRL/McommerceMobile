using System;
using System.IO;
using AppCentreXamTest.Droid.DependencyService;
using ShoppingCart.DataService;
using SQLite;
using Xamarin.Forms;

[assembly: Dependency(typeof(AndroidLocalStorage))]

namespace AppCentreXamTest.Droid.DependencyService
{
    public class AndroidLocalStorage : ILocalStorage
    {
        public SQLiteConnection GetConnection()
        {
            var path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            path = Path.Combine(path, "ShoppingKart.db3");
            var connection = new SQLiteConnection(path);
            return connection;
        }
    }
}