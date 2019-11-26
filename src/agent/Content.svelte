<script>
  import Router from "svelte-spa-router";
  import CallQueueContent from './CallQueue/Content.svelte';
  import CallSessionContent from './CallSession/Content.svelte';

  const routes = {
    '/': CallQueueContent,
    '/call': CallSessionContent
  };
</script>

<style>
  main {
    flex: 1 0 auto;
    margin-top: 70px;
  }
</style>

<main>
  <div class="container">
    <Router {routes} />
  </div>
</main>
