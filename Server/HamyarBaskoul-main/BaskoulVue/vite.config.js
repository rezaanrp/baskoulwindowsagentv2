import { defineConfig } from 'vite'
import vue from '@vitejs/plugin-vue'
import { resolve } from 'node:path'

export default defineConfig({
  plugins: [vue()],
  build: {
    outDir: resolve(__dirname, '../WebUI/wwwroot/baskoul-vue'),
    emptyOutDir: true,
    rollupOptions: {
      output: {
        entryFileNames: 'assets/index.js',
        assetFileNames: asset => asset.name?.endsWith('.css') ? 'assets/index.css' : 'assets/[name][extname]'
      }
    }
  }
})
