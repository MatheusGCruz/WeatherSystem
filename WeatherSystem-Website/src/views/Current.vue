<script setup lang="ts">
import { library } from '@fortawesome/fontawesome-svg-core';
import { FontAwesomeIcon } from '@fortawesome/vue-fontawesome';
import { fas } from '@fortawesome/free-solid-svg-icons';
import axios from 'axios';
import Forecast from '../views/Forecast.vue';
import { onMounted, ref } from 'vue';

interface ApiResponse {
  id: number;
  name: string;
  url: string;
}

library.add(fas);
defineProps<{
  msg2: string;
  subdirectory: {
      type: String,
      required: true
    };
}>()


const fcity = "araguari";
const city = String(fcity).charAt(0).toUpperCase() + String(fcity).slice(1);
const temperature = 29;
const preciptation = 0;
const cloudCover = 25;
const humidity = 45;
const uvIndex = 15;
const visibility = 10;
const subdirectory = ref<string>('');  // Reactive reference to store subdirectory
const loading = ref<boolean>(true);
const day10 ={
  minTemp:20,
  maxTemp:30,
  humidity:50,
}
const day0 = ref<Object>(day10);
const day1 = ref<Object>(day10);
const day2 = ref<Object>(day10);
const report  = ref<string | null>(null);



</script>

<template>
    <div class="greetings">
      <h1>Forecasts:</h1>
        <Forecast day="0" /><br/><br/>
        <Forecast day="1" /><br/><br/>
        <Forecast day="2" /><br/><br/>
    </div>
    <div>
     {{ report }}
    </div>
  </template>
<style scoped>
h1 {
  font-weight: 500;
  font-size: 2.6rem;
  position: relative;
  top: -10px;
  text-align: center;
  color: navy;
  background-image: url('/steel.jfif');
  background-size: 100% auto;
}

h3 {
  font-size: 1.2rem;
}
.greetings{
  background-image: url('/rust.jpg');
  background-size: 100% auto;
  margin:2px;
  border-radius: 20px;
  padding:1vh;
}
.greetings h1 {
  text-align: center;
  color: navy;
  background-image: url('/steel.jfif');
  background-size: 100% auto;
}
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