import $axios from "core-js/internals/internal-state";

const initState = () => ({
  message: "init"
});

export const state = initState;

export const mutations = {
  setMessage(state, message) {
    state.message = message;
  },
  reset(state) {
    Object.assign(state, initState())
  }
}

export const actions = {
  async fetchMessage({commit, dispatch}) {
    const message = (await this.$axios.get("http://localhost:5000/api/home")).data;
    commit("setMessage", message);
    await dispatch("tricks/fetchTricks");
  }
}
