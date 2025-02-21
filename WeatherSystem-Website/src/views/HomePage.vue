<template>
    <div class="greetings">
      <h1>Current Weather:</h1>
        <h2>City: {{city}} - {{ country }}</h2>
        <br/>
        <div class="icon-container">
            <font-awesome-icon :icon="['fas', 'fa-thermometer-empty']" /> Temperature: {{temperature}}ºC - Sensation {{ feeling }}ºC
            <span class="tooltip">Temperature (in ºC)</span>
        </div>
        <br/><br/>

        <div class="icon-container">
            <font-awesome-icon :icon="['fas', 'fa-cloud']" /> Cloud Coverage: {{cloudCover}}%
            <span class="tooltip">Cloud Coverage</span>
        </div>
        <br/><br/>
        <div class="icon-container">
            <font-awesome-icon :icon="['fas', 'fa-tint']" /> Precipitation: {{preciptation}}mm
            <span class="tooltip">Preciptation (in mm)</span>
        </div>
        <br/><br/>
        <div  class="icon-container">
            <font-awesome-icon :icon="['fas', 'fa-sun']" /> UV Index: {{uvIndex}}
            <span class="tooltip">Ultra Violet Index</span>
        </div>
        <br/><br/>  
        <div  class="icon-container">
            <font-awesome-icon :icon="['fas', 'fa-clock']" /> Observation Date: {{obsDate}}
            <span class="tooltip">Date and time of the measurement</span>
        </div>
        <br/><br/>   
        <div  class="icon-container">
            <font-awesome-icon :icon="['fas', 'fa-clock']" /> Local time: {{ localDate }}
            <span class="tooltip">Date and time of the measurement (in the local time offset )</span>
        </div>
        <br/><br/>  
        <div  class="icon-container">
            <font-awesome-icon :icon="['fas', 'fa-cog']" /> Shadow Date: {{shadowDate}}
            <span class="tooltip">Date and time for the last check</span>
        </div>
        <br/><br/>  

    </div>
    <div>
      <a href="http://localhost:5173/araguari">
        Go to Araguari 
      </a>
      <a href="http://localhost:5173/araxa">
        Go to Araxa 
      </a>
      <a href="http://localhost:5173/uberlandia">
        Go to Uberlandia 
      </a>
    </div>
    <br/>
    <br/>

    <!-- <div>teste {{ subError }}</div> -->
    <p v-if="loading">Loading weather...</p>
    <p v-else-if="error" class="error">{{ error }}</p>
  </template>

<script lang="ts">
import { defineComponent, computed, onMounted, ref, watch } from 'vue';
import { useRoute } from 'vue-router';
import { fas } from '@fortawesome/free-solid-svg-icons';
import { library } from '@fortawesome/fontawesome-svg-core';
import { FontAwesomeIcon } from '@fortawesome/vue-fontawesome';
import axios from 'axios';

library.add(fas);

export default defineComponent({
  setup() {
    const route = useRoute();  // Access current route
    const subdirectory = ref<string>('');  // Reactive reference to store subdirectory


    
    const city = ref<string | null>(null);
    const country = ref<string | null>(null);
    const temperature = ref<string | null>(null);
    const preciptation = ref<string | null>(null);
    const cloudCover = ref<string | null>(null);
    const humidity = ref<string | null>(null);
    const uvIndex = ref<string | null>(null);
    const visibility = ref<string | null>(null);
    const loading = ref<boolean>(true);
    const error = ref<string | null>(null);
    const teste = ref<string | null>(null);
    const subError = ref<string | null>(null);
    const obsDate = ref<string | null>(null);
    const shadowDate = ref<string | null>(null);
    const localDate = ref<string | null>(null);
    const feeling = ref<string | null>(null);



    const fetchData = async () => {
      try {
        subdirectory.value = window.location.pathname.slice(1);
        subError.value = subdirectory.value;
        const response = await axios.get(`https://api.antares.ninja/weather/`+subdirectory.value);
        
        // Assuming the API returns an icon name or data that determines the icon to use
        teste.value = response.data.city; // Default to prop if no icon in response
        temperature.value = response.data.temp_C;
        preciptation.value = response.data.precipMM;
        uvIndex.value = response.data.uvIndex;
        localDate.value =  response.data.localObsDateTime;
        obsDate.value = response.data.observation_time;
        shadowDate.value = response.data.lastUpdateDate;
        city.value = response.data.city;
        country.value = response.data.country;
        visibility.value = response.data.visibility;
        humidity.value = response.data.humidity;
        cloudCover.value = response.data.cloudcover;
        feeling.value = response.data.feelsLikeC;


      
      } catch (err) {
        error.value = (err as Error).message || 'Failed to fetch icon';
      } finally {
        loading.value = false;
      }
    };

    onMounted(() => {
      fetchData();
    });

    return {country, localDate,obsDate,shadowDate,subError,subdirectory, city, temperature,preciptation,cloudCover,humidity,uvIndex,visibility, teste, loading, error, feeling };
  }

  
});
</script>

<style scoped>
h1 {
  font-weight: 500;
  font-size: 2.6rem;
  position: relative;
  top: -10px;
  color: navy;
}

h3 {
  font-size: 1.2rem;
}
.greetings{
  background-image: url('./src/assets/polished.png');
  background-size: 100% auto;
  padding:5%
}

.greetings h1,
.greetings h3 {
  text-align: center;
}

@media (min-width: 1024px) {
  .greetings h1,
  .greetings h3 {
    text-align: left;
  }
}

.icon-container {
  position: relative;
  display: inline-block;
  cursor: pointer;
}

.tooltip {
  visibility: hidden;
  background-color: black;
  color: #fff;
  text-align: center;
  padding: 5px 10px;
  border-radius: 5px;
  position: absolute;
  bottom: 100%;
  left: 50%;
  transform: translateX(-50%);
  opacity: 0;
  transition: opacity 0.3s;
  white-space: nowrap;
}

.icon-container:hover .tooltip {
  visibility: visible;
  opacity: 1;
}
</style>