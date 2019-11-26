<script>
  export let participants = [];
  export let waiting;
</script>

{#if participants.length === 2}
  <slot />
{:else}
  <svelte:component this={waiting} />
{/if}
