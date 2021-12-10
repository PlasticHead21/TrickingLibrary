<template>
  <v-dialog
    persistent
    :value="active"
    width="900"
  >
    <template v-slot:activator="{on}">
      <v-btn v-on="on" @click="toggleActivity">
        Upload
      </v-btn>
    </template>
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
            Name of trick
          </v-stepper-step>

          <v-divider></v-divider>

          <v-stepper-step
            :complete="step > 3"
            step="3"
          >
            Upload video
          </v-stepper-step>

          <v-divider></v-divider>

          <v-stepper-step
            :complete="step > 4"
            step="4">
            Trick information
          </v-stepper-step>

          <v-divider></v-divider>

          <v-stepper-step step="5">
            Review
          </v-stepper-step>
        </v-stepper-header>

        <v-stepper-items>
          <v-stepper-content step="1">
            <div class="d-flex flex-column align-center">
              <v-btn class="my-2" @click="setType({type: uploadType.TRICK})">Trick</v-btn>
              <v-btn class="my-2" @click="setType({type: uploadType.SUBMISSION})">Submission</v-btn>
            </div>
          </v-stepper-content>

          <v-stepper-content step="2">
            <div>
              <v-text-field label="Tricking Name" v-model="trickName"/>
              <v-btn @click="incStep">Submit</v-btn>
            </div>
          </v-stepper-content>

          <v-stepper-content step="3">
            <div>
              <v-file-input accept="video/*" @change="handleFile"></v-file-input>
            </div>
          </v-stepper-content>

          <v-stepper-content step="4">
            <div>
              <v-text-field label="Description" v-model="submission"/>
              <v-btn @click="incStep">Submit</v-btn>
            </div>
          </v-stepper-content>

          <v-stepper-content step="5">
            <v-btn @click="save">Save</v-btn>
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
    trickName: "",
    submission: ""
  }),
  computed: {
    ...mapState("videos", ['uploadTask', 'active', 'step', 'type']),
    uploadType() {
      return UPLOAD_TYPE;
    }
  },
  methods: {
    ...mapMutations("videos", ['toggleActivity', 'setType', 'reset', 'incStep']),
    ...mapActions("videos", ['startVideoUpload', 'createTrick']),
    async handleFile(file) {
      if (!file) return;

      const form = new FormData();
      form.append("video", file);
      this.startVideoUpload({form});
    },
    async save() {
      if (!this.uploadTask) {
        console.log("uploadTask is null.");
        return;
      }

      const video = await this.uploadTask;
      await this.createTrick(
        {
          trick: {name: this.trickName},
          submission: {
            description: this.submission,
            video,
            trickId: 1
          }
        });
      this.trickName = "";
      this.submission = "";
      this.reset();
    }
  }
}
</script>

<style scoped>

</style>
