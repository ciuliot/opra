<script>
  import i18n from "i18next";

  export let finishedCalls = [];
</script>

{#if finishedCalls.length > 0}
  <h4>{i18n.t('finished_calls')}</h4>
  <table class="striped" />
{/if}
