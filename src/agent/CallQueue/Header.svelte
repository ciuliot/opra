<script>
  import i18n from "i18next";
  import { workflowTypes, selectedWorkflowType } from "./store";

  let workflowTypes_value;
  let selectedWorkflowType_value;

  workflowTypes.subscribe(v => (workflowTypes_value = v));
  selectedWorkflowType.subscribe(v => (selectedWorkflowType_value = v));

  const localizationString = workflowType =>
    i18n.t(`${workflowType.replace("-", "_")}_workflow_name`);
</script>

<ul class="right hide-on-med-and-down">
  <li>
    <a class="dropdown-trigger" href="#!" data-target="workflow-type-values">
      {#if selectedWorkflowType_value}
        {localizationString(selectedWorkflowType_value)}
      {/if}
      <i class="material-icons right">arrow_drop_down</i>
    </a>
  </li>

  <ul id="workflow-type-values" class="dropdown-content">
    {#each workflowTypes_value as workflowType}
      <li>
        <a
          href="#!"
          on:click={() => (selectedWorkflowType.set(workflowType))}>
          {localizationString(workflowType)}
        </a>
      </li>
    {/each}
  </ul>
</ul>
