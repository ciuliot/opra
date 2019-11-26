import svelte from "rollup-plugin-svelte";
import resolve from "rollup-plugin-node-resolve";
import commonjs from "rollup-plugin-commonjs";
import jsonPlugin from "rollup-plugin-json";
import { terser } from "rollup-plugin-terser";
import postcss from "rollup-plugin-postcss";

const production = !process.env.ROLLUP_WATCH;

function generateConfig(part) {
  return {
    input: `src/${part}/main.js`,
    output: {
      sourcemap: true,
      format: "iife",
      name: "app",
      file: `public/build/${part}.js`
    },
    plugins: [
      postcss({
        extensions: [".css"]
      }),
      jsonPlugin(),
      svelte({
        // enable run-time checks when not in production
        dev: !production,
        // we'll extract any component CSS out into
        // a separate file — better for performance
        css: css => {
          css.write(`public/build/${part}.css`);
        }
      }),
  
      // If you have external dependencies installed from
      // npm, you'll most likely need these plugins. In
      // some cases you'll need additional configuration —
      // consult the documentation for details:
      // https://github.com/rollup/rollup-plugin-commonjs
      resolve({
        browser: true,
        dedupe: importee =>
          importee === "svelte" || importee.startsWith("svelte/")
      }),
      commonjs(),    
  
      // If we're building for production (npm run build
      // instead of npm run dev), minify
      production && terser()
    ],
    watch: {
      chokidar: false,
      clearScreen: false,
      include: [`src/${part}/**`, 'src/shared/**'],
      exclude: 'node_modules/**'
    }
  };
}

export default [
  generateConfig('agent'),
  generateConfig('kiosk-desktop'),
];

function serve() {
  let started = false;

  return {
    writeBundle() {
      if (!started) {
        started = true;

        require("child_process").spawn("npm", ["run", "start", "--", "--dev"], {
          stdio: ["ignore", "inherit", "inherit"],
          shell: true
        });
      }
    }
  };
}
