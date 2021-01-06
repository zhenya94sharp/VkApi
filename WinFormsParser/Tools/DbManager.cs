using System;
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
        public void AddFriendsToDb(VkCollection<User> friends)
        {
            using (VkBirthdayEntities db = new VkBirthdayEntities())
            {
                try
                {
                    foreach (var friend in friends)
                    {
                        if (friend.BirthDate != null)
                        {
                           Friend addFriend = db.Friends.FirstOrDefault(i => i.name == friend.FirstName + " " + friend.LastName);

                           int index = friend.BirthDate.LastIndexOf('.');

                           string date = friend.BirthDate.Substring(0, index);

                           addFriend.date = date;


                            /* db.Friends.Add(new Friend()
                             {
                                 name = friend.FirstName + " " + friend.LastName,
                                 date = friend.BirthDate
                             });
                             */
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
}
