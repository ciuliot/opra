<script>
  import i18n from "i18next";
  import M from "materialize-css";
  import { onMount, onDestroy } from "svelte";

  let element, instance;

  onMount(() => {
    instance = M.Modal.init(element);

    window.setTimeout(() => {
      instance.open();
    }, 0);
  });
  
  onDestroy(() => {
    instance.close();
  })
</script>

<div class="modal bottom-sheet" bind:this={element}>
  <div class="modal-content">
    <h4>{i18n.t('waiting_for_participants_header')}</h4>
    <div class="progress">
      <div class="indeterminate" />
    </div>
    <p>{i18n.t('waiting_for_participants')}</p>
  </div>
</div>
