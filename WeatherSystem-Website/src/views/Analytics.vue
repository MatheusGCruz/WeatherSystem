<template>
    <div class="p-4 baseGrid">
      <div class="mb-4 flex gap-4">
        <input
          v-model="search"
          type="text"
          placeholder="Search..."
          class="border p-2 rounded w-full"
        />
        <select v-model="sortKey" class="border p-2 rounded">
          <option value="city">City</option>
          <option value="tempC">Temperature</option>
          <option value="date">Date</option>
          <option value="windspeedKmph">Wind Speed</option>
          <option value="humidity">Humidity</option>
          <option value="chanceofrain">Chance of Rain</option>
          <option value="uvIndex">UV Index</option>
        </select>
        <button @click="toggleSortOrder" class="p-2 border rounded bg-gray-200">
          {{ sortOrder === 'asc' ? '⬆️' : '⬇️' }}
        </button>
            </div>
      <div class="grid grid-cols-3 gap-4 max-h-96 overflow-y-auto ">
        <div v-for="item in filteredAndSortedItems" :key="item.id" class="p-4 border rounded shadow gridItem">
          <h3 class="text-lg font-bold">{{ item.city }}</h3>
          <p>[ Temperature: {{ item.tempC }}ºC ] 
            [ Humidity {{ item.humidity }}% ]
            [ Wind {{item.windspeedKmph }} Km/h {{ item.winddir16Point }} ]
            [ UV index {{ item.uvIndex }} ]
          </p>
          <p>
            Rain: [ chance: {{ item.chanceofrain }}% ]
            [ Preciptation: {{ item.precipMM }}mm ]
            [ Cloud Coverage: {{ item.cloudcover }} ]

          </p>
          <p>Date: {{ item.date }} {{ item.hour }}:00</p>
        </div>
      </div>
    </div>      
  </template>
  
  <script lang="ts">
import { ref, computed, defineComponent, onMounted } from "vue";
  
  interface Item {
    id: number;
    city: string;
    tempC: number;
    date: string;
    hour:number;
    humidity:number,
    winddir16Point:string;
    windspeedKmph:number;
    precipMM:number;
    chanceofrain:number;
    cloudcover:number;
    uvIndex:number;
  }
  
  export default defineComponent({
    setup() {
    const items = ref<Item[]>([]);

    const formatDate = (date: Date): string => {
      return date.toISOString().split('T')[0]; // Formats as YYYY-MM-DD
    };

    onMounted(() => {
      fetchAndAddItem();
    });

    const fetchAndAddItem = async () => {
      try {
        items.value = [];
        const response = await fetch("https://api.antares.ninja/analytics/");
        const data = await response.json();
        const user = data[0];
        data.forEach((element: { id: any; city: any; tempC: any; date: any; time: any; humidity:any ; winddir16Point:any; windspeedKmph:any; precipMM:any; chanceofrain:any; cloudcover:any; uvIndex:any}) => {
          let newdate = new Date(element.date);
          const newItem: Item = {
            id: element.id,
              city: element.city,
              tempC: element.tempC,              
              date: formatDate(newdate),
              hour: element.time/100,
              humidity : element.humidity,
              winddir16Point:element.winddir16Point,
              windspeedKmph:element.windspeedKmph,
              precipMM:element.precipMM,
              chanceofrain: element.chanceofrain,
              cloudcover:element.cloudcover,
              uvIndex:element.uvIndex,
            };
        items.value.push(newItem);
        console.log(items);
        });
      } catch (error) {
        console.error("Error fetching user:", error);
      }
    };
      

      const search = ref<string>("");
      const sortKey = ref<keyof Item>("city");
      const sortOrder = ref<"asc" | "desc">("asc");
  
      const filteredAndSortedItems = computed(() => {
        return items.value
          .filter(item =>
            Object.values(item).some(val =>
              String(val).toLowerCase().includes(search.value.toLowerCase())
            )
          )
          .sort((a, b) => {
            let result = a[sortKey.value] > b[sortKey.value] ? 1 : -1;
            return sortOrder.value === "asc" ? result : -result;
          });
      });
  
      const toggleSortOrder = () => {
        sortOrder.value = sortOrder.value === "asc" ? "desc" : "asc";
      };
  
      return { search, sortKey, sortOrder, filteredAndSortedItems, toggleSortOrder, fetchAndAddItem};
    }
  });
  </script>
  
  <style>
.baseGrid{
  background-image: url('/rust.jpg');
  background-size: 100% auto;
  margin:2px;
  border-radius: 20px;
  padding:1vh;
}
.gridItem{
  background-image: url('/polished.png');
  background-size: 100% auto;
  padding: 1%;
  margin:2px;
  border-radius: 20px;
}
.grid{
  overflow-y: auto; 
  max-height: 40em;
}

.gridItem p{
  color:navy;
}

::-webkit-scrollbar {
  width: 20px;
}

/* Track */
::-webkit-scrollbar-track {
  box-shadow: inset 0 0 5px silver; 
  border-radius: 10px;
}
 
/* Handle */
::-webkit-scrollbar-thumb {
  background: navy; 
  border-radius: 10px;
}

/* Handle on hover */
::-webkit-scrollbar-thumb:hover {
  background: silver; 
}

button,
select,
input{
  color:navy;
  border-radius: 10px;
}
</style>