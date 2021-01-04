using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VkNet.Model;
using VkNet.Utils;

namespace WinFormsParser.Tools
{
    class DbManager
    {
        private VkBirthdayEntities db = new VkBirthdayEntities();
        public void AddFriendsToDb(VkCollection<User> friends)
        {
            try
            {
                foreach (var friend in friends)
                {
                    if (friend.BirthDate != null)
                    {
                        db.Friends.Add(new Friend()
                        {
                            name = friend.FirstName + " " + friend.LastName,
                            date = friend.BirthDate
                        });
                        db.SaveChanges();

                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Ошибка при добавлении в Бд, проверьте соединение с базой\n" + e.Message);
            }
           
        }
    }
}
