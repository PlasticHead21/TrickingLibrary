<template>
  <v-app dark>
    <v-app-bar
      app
      dense
    >
      <v-toolbar-title>Tricking library</v-toolbar-title>
      <v-spacer></v-spacer>
      <video-upload></video-upload>
    </v-app-bar>
    <v-main>
      <v-container>
        <Nuxt/>
      </v-container>
    </v-main>
  </v-app>
</template>

<script>
import VideoUpload from "../components/video-upload";

export default {
  components: {
    VideoUpload
  }
}
</script>
