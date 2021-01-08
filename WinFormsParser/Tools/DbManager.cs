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
        public void UpdateFriendsToDb(VkCollection<User> friends)
        {
            using (VkBirthdayEntities1 db = new VkBirthdayEntities1())
            {
                try
                {
                    int id = db.Friends.FirstOrDefault().id;

                    foreach (var friend in friends)
                    {
                        if (friend.BirthDate != null)
                        {
                             id = db.Friends.FirstOrDefault(i => i.id == id).id;
                             Friend updateFriend = db.Friends.FirstOrDefault(i => i.id == id);

                             DateTime birthday = CreateDateTime(friend);

                             string name = friend.FirstName + " " + friend.LastName;

                             //если данные не совпадают то обновляем
                             if (updateFriend.birthday != birthday || updateFriend.name != name)
                             {
                                 updateFriend.idUser = (int)(friend.Id);
                                 updateFriend.birthday = birthday;
                                 updateFriend.name = name;
                                 db.SaveChanges();
                             }

                             id++;

                           /* //первое добавление

                            db.Friends.Add(new Friend()
                            {
                                idUser = (int)friend.Id,
                                name = friend.FirstName + " " + friend.LastName,
                                birthday = CreateDateTime(friend)
                            });*/

                            db.SaveChanges();
                        }
                        
                    }

                    MessageBox.Show("Данные успешно добавлены в Бд");
                }
                catch (Exception e)
                {
                    MessageBox.Show("Ошибка при добавлении в Бд, проверьте соединение с базой\n" + e.Message);
                }
            }
        }


        private DateTime CreateDateTime(User friend)
        {
            int indexSubstring;
            string birthdayVk=friend.BirthDate;

            if (friend.BirthDate.Length > 5)
            {
                indexSubstring = friend.BirthDate.LastIndexOf('.');
                birthdayVk = birthdayVk.Substring(0, indexSubstring);
            }

            string[] dayMounth = birthdayVk.Split('.');

            indexSubstring = DateTime.Today.ToString().LastIndexOf('.');

            int year = int.Parse(DateTime.Today.ToString().Substring(indexSubstring+1, 4));


            DateTime date = new DateTime(year, int.Parse(dayMounth[1]), int.Parse(dayMounth[0]));

            return date;
        }



    }
}

