<script>
  import { HubConnectionBuilder, HubConnectionState, LogLevel } from "@microsoft/signalr";
  import { onMount, onDestroy, createEventDispatcher } from 'svelte';

  export let role;

  const dispatch = createEventDispatcher();  

  const connection = new HubConnectionBuilder()
    .withUrl("/callSession")
    .configureLogging(LogLevel.Information)
    .withAutomaticReconnect()
    .build();

  connection.on("participantsChanged", participants => {
    console.log(`Participants changed to`, participants);
    dispatch('participants-changed', participants);
  });

  async function registerParticipant() {
    console.log('Registering as', role);
    await connection.invoke('RegisterParticipant', role);
  }

  onMount(async () => {
    await connection.start();
    await registerParticipant();
  });

  onDestroy(async () => {
    await connection.stop();
  });

  $: {
    if (connection.state === HubConnectionState.Connected) {
      registerParticipant();
    }
  }

</script>
