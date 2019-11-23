<script>
  import Header from "./Header.svelte";
  import FinishedCalls from "./FinishedCalls.svelte";
  import QueuedCalls from "./QueuedCalls.svelte";
  import { selectedWorkflowType } from "./store";
  import i18n from "i18next";
  import localization from "./localization";
  import pkg from "../../package.json";

  let workflowTypes = ["op", "remote-advisor"];
  selectedWorkflowType.set(workflowTypes[0]);  

  let isLoaded = false;

  i18n.init(
    {
      lng: "en",
      debug: true,
      resources: localization
    },
    (err, t) => {
      isLoaded = true;
    }
  );
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

  main {
    flex: 1 0 auto;
    margin-top: 70px;
  }

  footer {
    height: 60px;
  }
</style>

{#if isLoaded}
  <Header {workflowTypes} />

  <main>
    <div class="container">
      
      <QueuedCalls />
      <FinishedCalls />
    </div>
  </main>

  <footer class="page-footer">
    <div class="container">
      <span>© Copyright 2019 Digitalist USA, Plc.</span>
      <div class="right">{i18n.t('version_format', {version: pkg.version})}</div>
    </div>
  </footer>
{/if}
