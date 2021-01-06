export let tableFriends={
    data(){
        return{
            friendsList:[],
            data:null
        }
    },
    template:`
        <div>
            
              
            <button class="btn btn-danger" v-on:click="friendsLoad">Загрузить погоду</button>

        <p v-if="friendsList!=null">{{friendsList}}</p>

        </div>   
    `,
    methods:{
        friendsLoad:async function (){
            let response = await fetch("https://localhost:44335/Vk",
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
                this.friendsList=await response.json()
                alert("Получены данные о погоде");
            }
            else
            {
                alert("При загрузке данных произошла ошибка "+response.status)
            }
        }
    }
}


