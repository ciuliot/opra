<script>
  import Hub from "./Hub.svelte";
  import QueuedCalls from "./QueuedCalls.svelte";
  import FinishedCalls from "./FinishedCalls.svelte";    

  let finishedCalls = [];    
</script>

<Hub bind:finishedCalls />

<QueuedCalls />
<FinishedCalls {finishedCalls} />
