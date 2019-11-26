<script>
  import Hub from "../../shared/CallSessionHub.svelte";
  import WaitingForParticipants from "./WaitingForParticipants.svelte";
  import ParticipantsConnected from "../../shared/ParticipantsConnected.svelte";

  let participants;

  function onParticipantsChanged(event) {
    participants = event.detail;
  }
</script>

<Hub role={0} on:participants-changed={onParticipantsChanged} />

<ParticipantsConnected {participants} waiting={WaitingForParticipants}>
</ParticipantsConnected>

