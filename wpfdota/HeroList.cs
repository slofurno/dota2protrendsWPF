using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;



namespace wpfdota
{
    public class HeroList : ObservableCollection<Hero>
    {

        public HeroList() 
        {

            Init();

        }

        private async Task Init()
        
        {

            using (var client = new HttpClient())
            {
                // New code:

                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                
                HttpResponseMessage response = await client.GetAsync("http://dota2protrends.azurewebsites.net/api/herolist/");

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsAsync<IEnumerable<Hero>>();

                    foreach (var hero in content)
                    {
                        this.Add(hero);

                    }


                }

            }

        }
    }

    public class MatchList : ObservableCollection<GamePlayerModel>
    {

        public MatchList()
        {

            Fetch(2);

        }

        public void Fetch(int id){

            this.ClearItems();

            fetch(id);

        }

        private async Task fetch(int id)
        {

            using (var client = new HttpClient())
            {
                // New code:

                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync("http://dota2protrends.azurewebsites.net/api/matchesbyhero/"+ id);

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsAsync<IEnumerable<GamePlayerModel>>();

                    foreach (var hero in content)
                    {
                        this.Add(hero);

                    }


                }

            }

        }
    }


    public class GamePlayerModel
    {
        
        public int id { get; set; }
        public int playerid { get; set; }
        public virtual Match match { get; set; }
        public virtual Player player { get; set; }
        public virtual ICollection<Item> items { get; set; }
        public virtual Hero hero { get; set; }
        public int kills { get; set; }
        public int deaths { get; set; }
        public int assists { get; set; }
        public int playerslot { get; set; }

        public GamePlayerModel()
        {


        }


    }

    public class Player
    {

        public Player(long pid, string name)
        {
            this.name = name;
            this.playerident = pid;

        }

        public Player()
        {


        }

        public int id { get; set; }
        public long playerident { get; set; }
        public string name { get; set; }



    }

    public class Match
    {
        
        public int id { get; set; }
        
        public int matchnumber { get; set; }
        
        public bool radiantwon { get; set; }
        
        
        public int gamemode { get; set; }

       

        public Match()
        {

        }
    }

    public class Item
    {
       
        
        public int itemnumber { get; set; }

        
        public virtual ItemNames iteminfo { get; set; }


        public Item()
        {

        }
    }

    public class ItemNames
    {


        
        public string itemurl { get; set; }

        public string fullurl
        {
            get
            {
                return "http://cdn.dota2.com/apps/dota2/images/items/" + this.itemurl;
            }  
        }

        public ItemNames()
        {

        }

        


    }

    public class Hero
    {

        //public int id { get; set; }
        
        public int heronumber { get; set; }
        
        public string heroname { get; set; }

        public Hero()
        {

        }
    }

}
