<script>
  import Router from "svelte-spa-router";
  import CallQueueHeader from './CallQueue/Header.svelte';
  import i18n from "i18next";

  const routes = {
    '/': CallQueueHeader,
  };
</script>

<style>
  header {
    position: fixed;
    top: 0;
    right: 0;
    left: 0;
    z-index: 2;
  }
</style>

<header>
  <div class="navbar-fixed">
    <nav>
      <div class="nav-wrapper container">
        <a class="brand-logo left" href="/">{i18n.t('remote_advisor')}</a>
        <Router {routes} />
      </div>
    </nav>
  </div>
</header>
