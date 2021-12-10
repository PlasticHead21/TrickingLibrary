import $axios from "core-js/internals/internal-state";

const initState = () => ({
  submissions: []
});

export const state = initState;

export const mutations = {
  setSubmissions(state, {submissions}) {
    state.submissions = submissions;
  },
  reset(state) {
    Object.assign(state, initState())
  }
}

export const actions = {
  async fetchSubmissions({commit}) {
    const submissions = (await this.$axios.get("/api/submissions")).data;
    console.log("tricks: ", submissions);
    commit("setSubmissions", {submissions});
  }
}
