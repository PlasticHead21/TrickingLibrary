<template>
  <v-card>
    <v-card-title class="headline">
    </v-card-title>
    <div v-if="tricks">
      <div v-for="t in tricks">
        {{ t.name }}
      </div>
    </div>

    <div v-if="submissions">
      <div v-for="s in submissions">
        {{ s.description }}
        <div>
          <video width="400" controls :src="`http://localhost:5000/api/videos/${s.video}`"></video>
        </div>
      </div>
    </div>
  </v-card>
</template>

<script>
import {mapState} from 'vuex';

export default {
  computed: {
    ...mapState("tricks", ['tricks']),
    ...mapState("submissions", ['submissions']),
  }
}
</script>
