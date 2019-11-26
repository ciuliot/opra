import { writable } from 'svelte/store';

export const workflowTypes = writable(['op', 'remote-advisor']);
export const selectedWorkflowType = writable('op');