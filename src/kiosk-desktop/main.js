import App from './App.svelte';
import M from 'materialize-css';
import 'materialize-css/dist/css/materialize.min.css'; 

const app = new App({
	target: document.body,
	props: {
		name: 'world'
	}
});

M.AutoInit();

export default app;