<script>
  import i18n from "i18next";

  import Router from "svelte-spa-router";
  import Header from "./Header.svelte";
  import Content from "./Content.svelte";
  import Localization from "../shared/Localization.svelte";

  import resources from "./localization";
  import pkg from "../../package.json";
</script>

<style>
  :global(body) {
    height: 100%;
    overflow: hidden;
    padding: 0px;
    display: flex;
    min-height: 100vh;
    flex-direction: column;
  }

  footer {
    height: 60px;
  }
</style>

<Localization {resources}>
  <Header />
  <Content />

  <footer class="page-footer">
    <div class="container">
      <span>© Copyright 2019 Digitalist USA, Plc.</span>
      <div class="right">
        {i18n.t('version_format', { version: pkg.version })}
      </div>
    </div>
  </footer>
</Localization>
