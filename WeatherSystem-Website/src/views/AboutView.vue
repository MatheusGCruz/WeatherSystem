<script setup lang="ts">
import WelcomeItem from './../components/WelcomeItem.vue'
import DocumentationIcon from './../components/icons/IconDocumentation.vue'
import ToolingIcon from './../components/icons/IconTooling.vue'
import EcosystemIcon from './../components/icons/IconEcosystem.vue'
import CommunityIcon from './../components/icons/IconCommunity.vue'
import SupportIcon from './../components/icons/IconSupport.vue'

</script>

<template>
  <div className="greetings">
  <WelcomeItem>
    <template #icon>
      <DocumentationIcon />
    </template>
    <template #heading>Documentation</template>
      This project was designed in Vue.js. <br/>The project also uctilizes a C# backend for API controller, and a C# HangFire as scheduler <br/>
  </WelcomeItem>

  <WelcomeItem>
    <template #icon>
      <ToolingIcon />
    </template>
    <template #heading>Tooling</template>

    This project is served and bundled with
    <a href="https://vite.dev/guide/features.html" target="_blank" rel="noopener">Vite</a>. The backend is server with a C# controller, hosted on premises, and routed throught <a href="https://cloudflare.com" target="_blank" rel="noopener">Cloudflare</a>

    <br />


  </WelcomeItem>

  <WelcomeItem>
    <template #icon>
      <EcosystemIcon />
    </template>
    <template #heading>Ecosystem</template>

    Access the Weather API throught the link:<br/>
    <a href="https://api.antares.ninja/weather" target="_blank" rel="noopener">Weather</a> (pass the city on the URL)<br/><br/>
    Access the Forecast API throught the link:<br/>
    <a href="https://api.antares.ninja/weather" target="_blank" rel="noopener">Forecast</a> (pass the city on the URL)<br/><br/>

  </WelcomeItem>


  <WelcomeItem>
    <template #icon>
      <SupportIcon />
    </template>
    <template #heading>About me</template>

    If you want to know more about this, or other projects, checkouy my social links:
    <a href="https://www.linkedin.com/in/matheusgcruz/" target="_blank" rel="noopener">  <font-awesome-icon :icon="['fas', 'fa-linkedin']" />Linkedin</a>
    <a href="https://github.com/MatheusGCruz" target="_blank" rel="noopener">  <font-awesome-icon :icon="['fas', 'fa-github']" />GitHub</a>
    <a href="https://matheus.antares.ninja" target="_blank" rel="noopener">  <font-awesome-icon :icon="['fas', 'fa-sitemap ']" />Resume</a>
  </WelcomeItem>
</div>
</template>

<style>
.greetings{
  background-image: url('./../assets/rust.jpg');
  background-size: 100% auto;
  margin:2px;
  border-radius: 20px;
  padding:1vh;
  font-weight: bold;
  border-color: silver;
}
</style>