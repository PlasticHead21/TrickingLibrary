import $axios from "core-js/internals/internal-state";

const initState = () => ({
  uploadTask: null
});

export const state = initState;

export const mutations = {
  setTask(state, {uploadTask}) {
    state.uploadTask = uploadTask;
  },
  reset(state) {
    Object.assign(state, initState())
  }
}

export const actions = {
  startVideoUpload({commit, dispatch}, {form}) {
    const uploadTask = this.$axios.$post("http://localhost:5000/api/videos", form);
    commit("setTask", {uploadTask});
  }
}
