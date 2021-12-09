<template>
  <v-dialog
    persistent
    :value="active"
    width="900"
  >
    <v-card>
      <v-stepper v-model="step">
        <v-stepper-header>
          <v-stepper-step
            :complete="step > 1"
            step="1"
          >
            Select type
          </v-stepper-step>

          <v-divider></v-divider>

          <v-stepper-step
            :complete="step > 2"
            step="2"
          >
            Upload video
          </v-stepper-step>

          <v-divider></v-divider>

          <v-stepper-step :complete="step > 3" step="3">
            Trick information
          </v-stepper-step>

          <v-divider></v-divider>

          <v-stepper-step step="4">
            Review
          </v-stepper-step>
        </v-stepper-header>

        <v-stepper-items>
          <v-stepper-content step="1">
            <div class="d-flex flex-column align-center">
              <v-btn class="my-2" @click="setType(uploadType.TRICK)">Trick</v-btn>
              <v-btn class="my-2" @click="setType(uploadType.SUBMISSION)">Submission</v-btn>
            </div>
          </v-stepper-content>

          <v-stepper-content step="2">
            <div>
              <v-file-input accept="video/*" @change="handleFile"></v-file-input>
            </div>
          </v-stepper-content>

          <v-stepper-content step="3">
            <div>
              <v-text-field label="Tricking Name" v-model="trickName"/>
              <v-btn @click="saveTrick">Save</v-btn>
            </div>
          </v-stepper-content>

          <v-stepper-content step="4">
            <div>Success</div>
          </v-stepper-content>
        </v-stepper-items>
      </v-stepper>
      <div class="d-flex justify-center">
        <v-btn @click="toggleActivity">Close</v-btn>
      </div>
    </v-card>
  </v-dialog>
</template>

<script>
import {mapMutations, mapActions, mapState} from 'vuex';
import {UPLOAD_TYPE} from '@/data/enums';

export default {
  name: "video-upload",
  data: () => ({
    trickName: ""
  }),
  computed: {
    ...mapState("videos", ['uploadTask', 'active', 'step']),
    uploadType() {
      return {...UPLOAD_TYPE}
    }
  },
  methods: {
    ...mapMutations("videos", ['toggleActivity', 'setType', 'reset']),
    ...mapActions("videos", ['startVideoUpload', 'createTrick']),
    async handleFile(file) {
      if (!file) return;

      const form = new FormData();
      form.append("video", file);
      this.startVideoUpload({form});
    },
    async saveTrick() {
      if (!this.uploadTask) {
        console.log("uploadTask is null.");
        return;
      }

      const video = await this.uploadTask;
      await this.createTrick({trick: {name: this.trickName, video}});
      this.trickName = "";
      this.reset();
    }
  }
}
</script>

<style scoped>

</style>
