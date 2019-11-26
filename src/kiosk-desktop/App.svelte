<script>
  import Localization from "../shared/Localization.svelte";
  import CallSessionHub from "../shared/CallSessionHub.svelte";

  import resources from "./localization";
</script>

<Localization {resources}>
  <CallSessionHub role={1} />
</Localization>