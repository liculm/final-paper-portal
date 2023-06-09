<template>
  <div class="home">
    <div class="sidebar left-sidebar">
      <div v-for="(button, index) in buttons" :key="index" class="button-container">
        <Button @click="openComponent(index)" class="button-item" size="small">
          {{ button.label }}
        </Button>
        <br />
        <i
          v-if="index !== buttons.length - 1"
          class="pi pi-arrow-down"
          style="color: slateblue"
        ></i>
      </div>
    </div>
    <div class="content">
      <div class="sidebar middle-sidebar">
        <div class="heading">
          <div v-if="selectedComponentIndex !== null">
            <component :is="selectedComponent"></component>
          </div>
        </div>
      </div>
    </div>

    <div class="sidebar right-sidebar">
      <h1>Trenutni status</h1>
      <div class="display-flex">
        <Avatar icon="pi pi-user" class="mr-2" size="large" shape="circle" />
        <p class="paragraph">{{ store.user.firstName }} {{ store.user.lastName }}</p>
        <p class="paragraph">{{ store.user.roleName }}</p>
      </div>

      <div class="display-flex-w">
        <p><b>Tema završnog rada: </b></p>
        <p>Tema</p>
      </div>
      <div class="display-flex">
        <p><b>Mentor: </b></p>
        <p>Marko Marić</p>
      </div>
      <div class="display-flex">
        <p><b> Status završnog rada: </b></p>
        <p>U izradi</p>
      </div>
      <div class="display-flex">
        <p><b> Dužnosti: </b></p>
        <p>Knjižnica</p>
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
import { ref } from 'vue'
import moment from 'moment'
import userController from '@/controllerEndpoints/userController'
import OdabirMentora from '@/views/HomePageViews/OdabirMentora.vue'
import OdabirTeme from '@/views/HomePageViews/OdabirTeme.vue'
import MentorITemaPrihvaceni from '@/views/HomePageViews/MentorITemaPrihvaceni.vue'
import Obrada from '@/views/HomePageViews/Obrada.vue'
import Duznosti from '@/views/HomePageViews/Duznosti.vue'
import ObranaRada from '@/views/HomePageViews/ObranaRada.vue'

const events = ref([
  {
    status: 'Odabir teme',
    date: '15/10/2020 10:30',
    icon: 'pi pi-shopping-cart',
    color: '#9C27B0'
  },
  { status: 'Odabir mentora', date: '15/10/2020 14:00', icon: 'pi pi-cog', color: '#673AB7' },
  {
    status: 'Mentor i tema prihvaćeni',
    date: '15/10/2020 16:15',
    icon: 'pi pi-shopping-cart',
    color: '#FF9800'
  },
  {
    status: 'U obradi: Izrada završnog rada u tijeku.',
    date: '16/10/2020 10:00',
    icon: 'pi pi-check',
    color: '#607D8B'
  },
  { status: 'Dužnosti', date: '15/10/2020 16:15', icon: 'pi pi-shopping-cart', color: '#FF9800' },
  { status: 'Obrana rada', date: '15/10/2020 16:15', icon: 'pi pi-shopping-cart', color: '#FF9800' }
])

export default {
  name: 'HomeView',
  data() {
    return {
      store: useUserStore(),
      events,
      selectedDate: moment().add(1, 'days').format('YYYY-MM-DD'),
      currentDate: moment().format('YYYY-MM-DD'),
      buttons: [
        { label: 'Odabir Mentora', component: OdabirMentora },
        { label: 'Odabir Teme', component: OdabirTeme },
        { label: 'Mentor I Tema Prihvaceni', component: MentorITemaPrihvaceni },
        { label: 'U obradi', component: Obrada },
        { label: 'Dužnosti', component: Duznosti },
        { label: 'Obrana Rada', component: ObranaRada }
      ],
      selectedComponentIndex: null
    }
  },
  methods: {
    async log() {
      const response = await userController.getTest()

      console.log(response)
    },

    openComponent(index) {
      this.selectedComponentIndex = index
    }
  },

  computed: {
    selectedComponent() {
      if (this.selectedComponentIndex !== null) {
        return this.buttons[this.selectedComponentIndex].component
      }
      return null
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
  height: 100vh;
}

.left-sidebar {
  padding-top: 40px;
  width: 220px;
  background-color: #f5f5f5;
  border-right: 1px solid #ccc;

  display: flex;
  flex-direction: column;
  justify-content: center;
  align-items: center;
}

.right-sidebar {
  padding-top: 40px;
  padding-left: 10px;
  width: 400px;
  background-color: #f5f5f5;
  border-left: 1px solid #ccc;
}

.content {
  flex: 1;
  padding: 30px;
}

.paragraph {
  padding-left: 2em;
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
.display-flex-c {
  padding-top: 10px;
  display: flex;
  padding-bottom: 15px;
  padding-left: 20px;
  border-bottom: 1px solid #ccc;
}

.home h1 {
  text-align: left;
  font-size: 1.5rem;
  font-weight: bold;
  margin: 0;
  padding: 20px;
}

.button-container {
  display: flex;
  align-items: center;
  margin-bottom: 20px;
  flex-direction: column;
}

.button-item {
  background-color: gray;
  border-color: black;
  height: 30px;
}

.button-item i {
  color: gray;
}
</style>
