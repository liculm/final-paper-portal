<template>
  <div class="home">
    <div class="sidebar left-sidebar">
      <div class="timeline-container">
        <Timeline :value="events" class="w-full md:w-20rem">
          <template #content="slotProps">
            {{ slotProps.item.status }}
          </template>
        </Timeline>
      </div>
    </div>
    <div class="content">
      <div class="sidebar middle-sidebar">
        <div class="heading">
          <h1>Naslovna stranica</h1>
        </div>
      </div>
    </div>

    <div class="sidebar right-sidebar">
      <h1>Trenutni status</h1>
      <p>Student</p>
      <div class="display-flex">
        <Avatar icon="pi pi-user" class="mr-2" size="large" shape="circle" />
        <p class="paragraph">User name</p>
      </div>

      <div class="display-flex-w">
        <p><b>Tema završnog rada: </b></p>
        <p>Tema</p>
      </div>
      <div class="display-flex">
        <p><b>Mentor: </b></p>
        <p> Marko Marić</p>
      </div>
      <div class="display-flex">
        <p><b> Status završnog rada: </b></p>
        <p> U izradi</p>
      </div>
      <div class="display-flex">
        <p><b> Dužnosti: </b></p>
        <p> Knjižnica</p>
      </div>
      <div class="display-flex-w">
        <h2>Obrana završnog rada</h2>

      </div>
      <div class="display-flex-w">
        <i class="pi pi-calendar" style="font-size: 2.5rem"></i>
        <p class="paragraph">15.07.2023.</p>
      </div>
      <div class="display-flex-w">
        <i class="pi pi-clock" style="font-size: 2.5rem"></i>
        <p class="paragraph">16.30h</p>
      </div>
      <div class="display-flex">
        <i class="pi pi-building" style="font-size: 2.5rem"></i>
        <p class="paragraph">Veleučilište u Rijeci - predavaonica 429</p>
      </div>
      <div class="display-flex-c">
        <Calendar v-model="selectedDate" inline :showWeek="false" />
      </div>


    </div>



  </div>

</template>

<script>
import { useUserStore } from '@/store/store'
import { ref } from "vue";
import Timeline from 'primevue/timeline';
import Avatar from 'primevue/avatar';
import Calendar from 'primevue/calendar';
import moment from 'moment';
import userController from '@/controllerEndpoints/userController'


const events = ref([
  { status: 'Odabir teme', date: '15/10/2020 10:30', icon: 'pi pi-shopping-cart', color: '#9C27B0'},
  { status: 'Odabir mentora', date: '15/10/2020 14:00', icon: 'pi pi-cog', color: '#673AB7' },
  { status: 'Mentor i tema prihvaćeni', date: '15/10/2020 16:15', icon: 'pi pi-shopping-cart', color: '#FF9800' },
  { status: 'U obradi: Izrada završnog rada u tijeku.', date: '16/10/2020 10:00', icon: 'pi pi-check', color: '#607D8B' },
  { status: 'Dužnosti', date: '15/10/2020 16:15', icon: 'pi pi-shopping-cart', color: '#FF9800' },
  { status: 'Obrana rada', date: '15/10/2020 16:15', icon: 'pi pi-shopping-cart', color: '#FF9800' },
]);

export default {
  name: 'HomeView',
  data () {
    return {
      store: useUserStore(),
      events,
      selectedDate: moment().add(1, 'days').format('YYYY-MM-DD'),
      currentDate: moment().format('YYYY-MM-DD'),
    }
  },
  components: {
    Timeline,
    Avatar,
    Calendar,
  },
  methods: {
    async log () {
      const response = await userController.getTest()

      console.log(response)
    }
  }
}
</script>


<style scoped>
.home {
  display: flex;
  flex-direction: row;
  height: 100vh;
  max-height: 60px;
}

.sidebar {
  padding: 10px;
  overflow-y: auto;
  height: max-content;
  height: 100vh;
}

.left-sidebar {
  padding-top: 40px;
  width: 220px;
  background-color: #f5f5f5;
  border-right: 1px solid #ccc;
  max-height: 650px;
}


.middle-sidebar{
  max-height: 620px;
}

.right-sidebar {
  padding-top: 40px;
  padding-left: 10px;
  width: 400px;
  background-color: #f5f5f5;
  border-left: 1px solid #ccc;
  max-height: 650px;
}

.timeline-container {
  flex: 1;
  padding: 20px;
  max-width: 0;
}

.content {
  flex: 1;
  padding: 30px;
}

.p-timeline {
  margin-left: 0;
}
.paragraph{
  padding-left: 10px;
}

.p-timeline-event-content {
  white-space: nowrap;
}

.timeline-item::before {
  left: 7px;
}

.timeline-container .p-timeline {
  margin-left: 0;
}

.display-flex {
  display: flex;
  padding-bottom: 15px;
  padding-left: 20px;
  border-bottom: 1px solid #ccc;
}

.display-flex-w {
  padding-left: 20px;
  display: flex;
  padding-bottom: 5px;
}
.display-flex-c{
  padding-top: 10px;
  display: flex;
  padding-bottom: 15px;
  padding-left: 20px;
  border-bottom: 1px solid #ccc;
}

.timeline-container{
  padding-left: 0;
  padding-right: 0;
}
.p-timeline-event-content{
  width: 150px;
  padding-right: 0;
}

/* New styles */
.home h1 {
  text-align: left;
  font-size: 1.5rem;
  font-weight: bold;
  margin: 0;
  padding: 20px;
}
</style>

