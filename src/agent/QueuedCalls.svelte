<script>
  import i18n from "i18next";

  export let queuedCalls = [];
</script>

<h3>{i18n.t('call_queue_header')}</h3>

{#if queuedCalls.length > 0}
  <table class="striped" />
{/if}
{#if queuedCalls.length == 0}
  <div class="card-panel success">
    {i18n.t('there_are_no_calls_in_the_queue')}
  </div>
{/if}
