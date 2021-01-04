export let tableWeather={
    data(){
        return{
            weatherList:[],
            weather:null,
            date:null
        }
    },
    template:`
        <div>
            
              
            <button class="btn btn-danger" v-on:click="weatherLoad">Загрузить погоду</button>

        <p v-if="weatherList!=null">{{weatherList}}</p>

        </div>   
    `,
    methods:{
        weatherLoad:async function (){
            let response = await fetch("https://api.vk.com/method/friends.getOnline?v=5.52&access_token=d805a7c9e56921f6e66419883844831d7636d723a710231a8834331d9fe247052d5e55d2218be54da130c",
                {
                    method:'GET',
					headers: {
						accept:'application/json',
						'Content-Type': 'application/json;charset=utf-8'
						}
                }
            );
            if(response.ok==true)
            {
                this.weatherList=await response.json()
                alert("Получены данные о погоде");
            }
            else
            {
                alert("При загрузке данных произошла ошибка "+response.status)
            }
        }
    }
}


