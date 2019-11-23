<script>
  import i18n from "i18next";
  import { selectedWorkflowType } from "./store";

  export let workflowTypes = [];
  let selectedWorkflowTypeValue;

  selectedWorkflowType.subscribe(value => {
    selectedWorkflowTypeValue = value;
  });

  const localizationString = workflowType =>
    i18n.t(`${workflowType.replace("-", "_")}_workflow_name`);
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
        <ul class="right hide-on-med-and-down">
          <li>
            <a
              class="dropdown-trigger"
              href="#!"
              data-target="workflow-type-values">
              {#if selectedWorkflowTypeValue}
                {localizationString(selectedWorkflowTypeValue)}
              {/if}
              <i class="material-icons right">arrow_drop_down</i>
            </a>
          </li>

          <ul id="workflow-type-values" class="dropdown-content">
            {#each workflowTypes as workflowType}
              <li>
                <a
                  href="#!"
                  on:click={() => selectedWorkflowType.set(workflowType)}>
                  {localizationString(workflowType)}
                </a>
              </li>
            {/each}
          </ul>
        </ul>
      </div>
    </nav>
  </div>
</header>
