# Orleans Blazor Shopping Cart - .NET 8.0 Testing Checklist

## Pre-Testing Setup
- [x] Build successful on .NET 8.0
- [ ] Application starts without errors
- [ ] Orleans silo initializes correctly
- [ ] Blazor Server connection establishes

## Core Functionality Tests

### 1. Application Startup & Orleans Cluster
**What to test:** Orleans clustering and grain initialization
**Steps:**
1. Run `dotnet run` in the Silo directory
2. Check console output for Orleans messages
3. Verify "Orleans Silo is running" or similar message
4. Look for any Orleans-related errors or warnings

**Expected:** Clean startup with Orleans silo running on localhost clustering

### 2. Blazor Server Connection
**What to test:** Blazor SignalR connection works with .NET 8.0
**Steps:**
1. Open browser to application URL (typically https://localhost:5001)
2. Check browser console for SignalR connection messages
3. Verify page loads without JavaScript errors
4. Test page interactivity (buttons, navigation)

**Expected:** Smooth page load with working SignalR connection

### 3. Authentication System (.NET 8.0 Compatibility)
**What to test:** Updated Microsoft Identity packages work correctly
**Steps:**
1. Navigate to shopping cart or protected areas
2. Test login flow with Azure B2C
3. Verify authentication redirects work
4. Check token handling and session management

**Expected:** Authentication works with new Identity.Web 4.3.0 packages

### 4. Orleans Grain Functionality
**What to test:** Core Orleans grains work with .NET 8.0
**Steps:**
1. Add products to cart (tests ShoppingCartGrain)
2. View product inventory (tests InventoryGrain)
3. Update product quantities
4. Remove items from cart
5. Check cart persistence across sessions

**Expected:** All grain operations work smoothly

### 5. Blazor Component Interactivity
**What to test:** Blazor components updated for .NET 8.0
**Steps:**
1. Navigate between Shop, Cart, and Products pages
2. Test product search/filtering
3. Add/remove cart items with real-time updates
4. Verify toast notifications work
5. Test modal dialogs (ManageProductModal)

**Expected:** All Blazor interactions work without issues

### 6. Data Persistence & State Management
**What to test:** Orleans grain state persists correctly
**Steps:**
1. Add items to cart
2. Close browser/restart application
3. Return and verify cart state is maintained
4. Test concurrent users (multiple browser tabs)

**Expected:** State management works correctly with .NET 8.0

## Performance & Compatibility Tests

### 7. .NET 8.0 Performance Features
**What to test:** Application leverages .NET 8.0 improvements
**Steps:**
1. Monitor application startup time
2. Check memory usage during operations
3. Test response times for grain operations
4. Monitor Blazor rendering performance

**Expected:** Good performance with .NET 8.0 optimizations

### 8. Package Compatibility Verification
**What to test:** All updated packages work correctly
**Steps:**
1. Test ApplicationInsights telemetry (updated to 2.23.0)
2. Verify JWT Bearer authentication (updated to 8.0.22)
3. Test OpenID Connect flows (updated to 8.0.22)
4. Check Microsoft Identity Web integration (updated to 4.3.0)

**Expected:** All package integrations work without issues

## Error Scenarios & Edge Cases

### 9. Error Handling
**What to test:** Application handles errors gracefully
**Steps:**
1. Test with invalid product IDs
2. Try accessing cart when Orleans is down
3. Test network interruptions with Blazor Server
4. Verify error pages render correctly

**Expected:** Graceful error handling with proper user feedback

### 10. Logging & Diagnostics
**What to test:** Logging works with .NET 8.0
**Steps:**
1. Check console output for Orleans logs
2. Verify Application Insights integration
3. Test different log levels (Information, Warning, Error)
4. Check structured logging output

**Expected:** Proper logging without issues

## Production Readiness Tests

### 11. Configuration Validation
**What to test:** Production configurations work
**Steps:**
1. Verify Azure Storage clustering config (for production)
2. Test Azure B2C authentication settings
3. Check Application Insights configuration
4. Validate cookie security settings

**Expected:** Production configurations are .NET 8.0 compatible

### 12. Security Verification
**What to test:** Security features work with updated packages
**Steps:**
1. Verify HTTPS enforcement
2. Test cookie security (SameSite, Secure flags)
3. Check authentication token validation
4. Test CORS policies if applicable

**Expected:** All security features work correctly

---

## Quick Start Testing Commands

```bash
# Navigate to project directory
cd "D:\orleans-blazor-server-shop\orleans-on-app-service\Silo"

# Run the application
dotnet run

# In another terminal, run load testing (if needed)
dotnet run --urls "https://localhost:5001;http://localhost:5000"
```

## Testing Notes
- Application runs on .NET 8.0 LTS (Long Term Support)
- Updated packages: Identity.Web (4.3.0), ApplicationInsights (2.23.0), Authentication packages (8.0.22)
- Orleans clustering: localhost for development, Azure Storage for production
- Blazor Server with SignalR for real-time updates

## Success Criteria
? Application starts without errors
? Orleans silo initializes successfully  
? Blazor pages load and interact correctly
? Shopping cart operations work (add/remove/persist)
? Authentication flows work with Azure B2C
? No runtime errors or exceptions
? Performance is acceptable or improved