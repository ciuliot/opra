<script>
  import i18n from "i18next";
  import { onMount } from "svelte";

  export let resources = {};

  let isLoaded = false;

  onMount(async () => {
    i18n.init(
      {
        lng: "en",
        debug: true,
        resources
      },
      (err, t) => {
        isLoaded = true;
      }
    );
  });
</script>

{#if isLoaded}
  <slot />
{/if}
