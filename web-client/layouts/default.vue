<template>
  <v-app dark>
    <v-app-bar
      app
      dense
    >
      <v-toolbar-title>Tricking library</v-toolbar-title>
      <v-spacer></v-spacer>
      <v-btn @click="toggleActivity">
        Upload
      </v-btn>
    </v-app-bar>
    <video-upload></video-upload>
    <v-main>
      <v-container>
        <Nuxt/>
      </v-container>
    </v-main>
  </v-app>
</template>

<script>
import {mapMutations} from 'vuex';
import VideoUpload from "../components/video-upload";

export default {
  methods: mapMutations('videos', ['toggleActivity']),
  components: {
    VideoUpload
  }
}
</script>
