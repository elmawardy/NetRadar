import Vue from 'vue';
import App from './App.vue';
import Vuetify from 'vuetify';

Vue.use(Vuetify)

window.app = new Vue({
    el: '#app',
    render: h => h(App)
})