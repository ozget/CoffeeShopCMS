import Vue from 'vue';
//import swal from 'sweetalert2';
import axios from 'axios';
new Vue({
    el: '#app',
    data: {
        config: {
            messages: [],
            showInfo: false,

            search: '',
            searchResults: [],
            selectionDisabled: false,
            selection: Number,
        },
        customer: {
        },
    },
    methods: {
        submit() {
            let self = this;
            console.log(this.customer);

           
            axios.post('/customer/create', this.customer)
                .then(function (response) {
                    self.config.customer = response.data;
                    self.config.showInfo = true;
                })
                .catch(function (error) {
                    console.log(error);
                }).then(function () {
                    self.config.loading = false;
                });
        },
    },
    created() {
    },

    computed: {
    }
});