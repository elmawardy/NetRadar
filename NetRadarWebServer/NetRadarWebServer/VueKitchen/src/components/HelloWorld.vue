<template>
  <v-container fluid>
    <v-slide-y-transition mode="out-in">
      <v-layout column align-center>
          <v-data-table :headers="headers"
                        :items="users"
                        hide-actions
                        class="elevation-1">
              <template slot="items" slot-scope="props">
                  <td>{{ props.item.name }}</td>
                  <td class="text-xs-right">{{ props.item.send }}</td>
                  <td class="text-xs-right">{{ props.item.recieve }}</td>
                  <td :style="{color:props.item.state == 'Connected'?'green':'black'}" class="text-xs-right">{{ props.item.state }}</td>
                  <td class="text-xs-right">{{ props.item.interface }}</td>
              </template>
          </v-data-table>
      </v-layout>
    </v-slide-y-transition>
  </v-container>
</template>

<script>


export default {
  name: 'HelloWorld',
  props: {
    msg: String
  },
  mounted(){
    setInterval(()=>{
      this.checkConnectivity();
    },1000)
  },
  data () {
  return {

      headers: [
          {
              text: 'User',
              align: 'left',
              sortable: false,
              value: 'user'
          },
          { text: 'Send KB/s', value: 'send' },
          { text: 'Recieve KB/s', value: 'recieve' },
          { text: 'State', value: 'state' },
          { text: 'Interface', value: 'interface' },
      ],
      users: []
    }
  },
  methods:{
    checkConnectivity(){
      for(var i =0;i<this.users.length;i++){
        if (new Date().getTime() - this.users[i].time > 60000) //if the user (client) hasn't send another packet for 1 minute
          this.users[i].state = 'Disconnected !';
      }
    }
  }
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
h3 {
  margin: 40px 0 0;
}
ul {
  list-style-type: none;
  padding: 0;
}
li {
  display: inline-block;
  margin: 0 10px;
}
a {
  color: #42b983;
}
</style>
