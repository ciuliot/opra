<script>
  import {
    HubConnectionBuilder,
    HubConnectionState,
    LogLevel
  } from "@microsoft/signalr";
  import { onMount, onDestroy } from "svelte";
  import { selectedWorkflowType } from "./store";

  const connection = new HubConnectionBuilder()
    .withUrl("/callQueue")
    .configureLogging(LogLevel.Information)
    .withAutomaticReconnect()
    .build();

  export let finishedCalls;
  let selectedWorkflowType_value;

  const selectWorkflow = async (workflowType) => {
    if (workflowType) {
      console.log(`Selected workflow type ${workflowType}`);

      if (connection.state === HubConnectionState.Connected) {
        await connection.invoke("SubscribeAgent", workflowType);
      }
    }
  }

  selectedWorkflowType.subscribe(value => {
    selectedWorkflowType_value = value;
    selectWorkflow(value);
  });

  connection.on("finishedCallsChanged", calls => {
    finishedCalls = calls;
  });

  onMount(async () => {
    await connection.start();
    await selectWorkflow(selectedWorkflowType_value);
  });

  onDestroy(async () => {
    await connection.stop();
  });
</script>
