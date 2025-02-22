<template>
  <div class="forecasts">
    <h2> Day: {{forecastDay}}</h2>
    
        <div class="icon-container">
            <font-awesome-icon :icon="['fas', 'fa-thermometer-empty']" /> Temperature: 
            <span class="tooltip">Temperature (in ºC)</span>
            Min: {{ minTempC }}ºC  / Avg: {{ medTempC }}ºC / Max: {{ maxTempC }}ºC 
        </div>
        <br/>
        <div class="icon-container">
            <font-awesome-icon :icon="['fas', 'fa-cloud']" /> UV Index: 
            <span class="tooltip">
              <p>Ultra Violet Index:</p>
              <p>0-2    - Low </p>
              <p>3-5    - Moderate</p>
              <p>6-7    - High</p>
              <p>8-10   - Very High</p>
              <p>11+    - Extreme</p>
            </span>
            {{ uvIndex }}
        </div>
        <br/>
        <div class="icon-container">
            <font-awesome-icon :icon="['fas', 'fa-sun']" /> Sun Hours: 
            <span class="tooltip">Sun hours (in ºC)</span>
            {{sunHour}} 
        </div>
        <br/>
        <div class="icon-container">
            <font-awesome-icon :icon="['fas', 'fa-snowflake']" /> Snow Coverage: 
            <span class="tooltip">Snow coverage in cm</span>
            {{totalSnow}} 
        </div>
        <br/>
        <div>
          <!-- {{ returnValue }} -->
        </div>
    </div>


</template>

<script lang="ts">
import axios from 'axios';
import { defineComponent, computed, onMounted, ref } from 'vue';
import { useRoute } from 'vue-router';

export default defineComponent({
    props: {
    day: {
      type: String,
      required: true
    },
  },
  setup(props) {
    const route = useRoute();
    const subdirectory = ref<string>('');
    const loading = ref<boolean>(true);
    // Get the subdirectory from the route params
    subdirectory.value = window.location.pathname.slice(1);
    const city = String(subdirectory).charAt(0).toUpperCase() + String(subdirectory).slice(1);
    const temperature = 29;
    const preciptation = 0;
    const cloudCover = 25;
    const humidity = 45;
    const uvIndex = ref<string>('');
    const visibility = 10;
    const minTempC = ref<string>('');
    const maxTempC = ref<string>('');
    const medTempC = ref<string>('');
    const returnValue = ref<string>('');
    const forecastDay = ref<string>('');
    const sunHour = ref<string>('');
    const totalSnow = ref<string>('');

    const fetchData = async () => {
      try {
        subdirectory.value = window.location.pathname.slice(1);
        const response = await axios.get(`https://api.antares.ninja/forecast/`+subdirectory.value);
        
        forecastDay.value = response.data[props.day].date;
        minTempC.value = response.data[props.day].mintempC;
        maxTempC.value = response.data[props.day].maxtempC;
        uvIndex.value = response.data[props.day].uvIndex;
        returnValue.value = response.data[props.day];
        medTempC.value = response.data[props.day].avgtempC;
        sunHour.value = response.data[props.day].sunHour;
        totalSnow.value = response.data[props.day].totalSnow_cm;





      } catch (err) {
        //error.value = (err as Error).message || 'Failed to fetch icon';
      } finally {
        loading.value = false;
      }
    };

    onMounted(() => {
      fetchData();
    });

    return {totalSnow, sunHour, forecastDay,returnValue,minTempC,maxTempC, medTempC, subdirectory, city, temperature,preciptation,cloudCover,humidity,uvIndex,visibility };
  }

  
});
</script>

<style scoped>
.forecasts{
  background-image: url('/polished.png');
  background-size: 100% auto;
  padding: 1%;
  margin:2px;
  border-radius: 20px;
}
h1 {
  font-weight: 500;
  font-size: 2.6rem;
  position: relative;
  top: -10px;
}

h3 {
  font-size: 1.2rem;
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

.tooltip p{
  background-color: black;
  color: #fff;
}

.icon-container:hover .tooltip {
  visibility: visible;
  opacity: 1;
}
</style>