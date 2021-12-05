<template>
  <v-card>
    <v-file-input accept="video/*" @change="handleFile"></v-file-input>
    <v-card-title class="headline">
      {{ message }}
      <v-btn @click="reset">Reset</v-btn>
      <v-btn @click="resetTricks">Reset tricks</v-btn>
    </v-card-title>
    <div v-if="tricks">
      <p v-for="t in tricks">
        {{ t.name }}
      </p>
    </div>
    <div>
      <v-text-field label="Tricking Name" v-model="trickName"/>
      <v-btn @click="saveTrick">Save</v-btn>
    </div>
  </v-card>
</template>

<script>
import {mapMutations, mapActions, mapState} from 'vuex';

export default {
  data: () => ({
    trickName: ""
  }),
  computed: {
    ...mapState({
      message: state => state.message
    }),
    ...mapState("tricks", {
      tricks: state => state.tricks
    }),
  },
  methods: {
    ...mapMutations([
      "reset"
    ]),
    ...mapMutations("tricks", {
      resetTricks: "reset"
    }),
    ...mapActions("tricks", ['createTrick']),
    async saveTrick() {
      await this.createTrick({trick: {name: this.trickName}});
      this.trickName = "";
    },
    async handleFile(file) {
      if (!file) return;

      const form = new FormData();
      form.append("video", file);

      const result = await this.$axios.post("http://localhost:5000/api/videos", form);
      console.log(result);
    }
  },
  // async fetch() {
  //   await this.$store.dispatch("fetchMessage");
  // }
}
</script>
