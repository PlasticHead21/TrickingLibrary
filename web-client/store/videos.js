const initState = () => ({
  uploadTask: null,
  active: false,
  step: 1,
  type: ""
});

export const state = initState;

export const mutations = {
  toggleActivity(state) {
    state.active = !state.active;

    if (!state.active)
      Object.assign(state, initState());
  },
  setType(state, {type}) {
    state.type = type;
    state.step++;
  },
  setTask(state, {uploadTask}) {
    state.uploadTask = uploadTask;
    state.step++;
  },
  reset(state) {
    Object.assign(state, initState())
  }
}

export const actions = {
  startVideoUpload({commit, dispatch}, {form}) {
    const uploadTask = this.$axios.$post("/api/videos", form);
    commit("setTask", {uploadTask});
  },
  async createTrick({commit, dispatch}, {trick}) {
    await this.$axios.post("/api/tricks", trick);
    await dispatch("tricks/fetchTricks", null, { root: true });
  }
}
